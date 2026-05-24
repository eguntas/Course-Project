using Aspire.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = DistributedApplication.CreateBuilder(args);






#region RABBITMQ
var rabbitmqUser = builder.AddParameter("RABBITMQ-USERNAME");
var rabbitmqPassword = builder.AddParameter("RABBITMQ-PASSWORD");

var rabbitmq = builder.AddRabbitMQ("rabbitMQ", rabbitmqUser, rabbitmqPassword, 5672).WithManagementPlugin(15672);

#endregion

#region KEYCLOAK
var postgresKeycloakdb = "keycloak_db";
var postgresUser = builder.AddParameter("POSTGRES-USER");
var postgresPassword = builder.AddParameter("POSTGRES-PASSWORD");

var postgresKeycloakDB = builder.AddPostgres("postgres-keycloak-db", postgresUser, postgresPassword, 5432)
    .WithVolume("postgres.db.keycloak.volume", "/var/lib/postgresql/data").AddDatabase(postgresKeycloakdb);

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "25.0")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "password")
    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KC_DB_URL", "jdbc:postgresql://postgres-db-keycloak/keycloak_db")
    .WithEnvironment("KC_DB_USERNAME", postgresUser)
    .WithEnvironment("KC_DB_PASSWORD", postgresPassword)
    .WithEnvironment("KC_HOSTNAME_PORT", "8080")
    .WithEnvironment("KC_HOSTNAME_STRICT_BACKCHANNEL", "false")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_HOSTNAME_STRICT_HTTPS", "false")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false")
    .WithEnvironment("KC_HEALTH_ENABLED", "true")
    .WithArgs("start").WaitFor(postgresKeycloakDB)
    .WithHttpEndpoint(8080, 8080, "keycloak-http-endpoint");

var keycloakEndpoint = keycloak.GetEndpoint("keycloak-http-endpoint");


#endregion

#region Catalog API with MongoDB
var mongoUser = builder.AddParameter("MONGO-USERNAME");
var mongoPassword = builder.AddParameter("MONGO-PASSWORD");
var catalogMongoDB = builder.AddMongoDB("mongo-catalog-db", 27030 , mongoUser, mongoPassword)
    .WithImage("mongo:8.0-rc").WithDataVolume("mongo.db.catalog.volume").AddDatabase("catalog-db");

var catalogApi = builder.AddProject<Projects.Course_Catalog_API>("course-catalog-api");
catalogApi.WithReference(catalogMongoDB).WaitFor(catalogMongoDB).WithReference(rabbitmq).WaitFor(rabbitmq).WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion

#region Basket API with Redis
var redisPassword = builder.AddParameter("REDIS-PASSWORD");

var redisBasketDB = builder.AddRedis("redis-db-basket")
    .WithImage("redis:7.0-alpine")
    .WithDataVolume("redis.db.basket.volume").WithPassword(redisPassword);
    
var basketApi = builder.AddProject<Projects.Course_Basket_API>("course-basket-api");
basketApi.WithReference(redisBasketDB).WithReference(rabbitmq).WaitFor(rabbitmq).WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Discount API with MongoDB

var discountMongoDB = builder.AddMongoDB("mongo-discount-db", 27034, mongoUser, mongoPassword)
    .WithImage("mongo:8.0-rc").WithDataVolume("mongo.db.discount.volume").AddDatabase("discount-db");

var discountApi = builder.AddProject<Projects.Course_Discount_API>("course-discount-api");
discountApi.WithReference(discountMongoDB).WaitFor(discountMongoDB).WithReference(rabbitmq).WaitFor(rabbitmq).WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion

#region Order API with SQL Server
var sqlServerPassword = builder.AddParameter("SQLSERVER-SA-PASSWORD");
var sqlserverOrderDB = builder.AddSqlServer("sqlserver-db-order").WithPassword(sqlServerPassword)
    .WithDataVolume("sqlserver.db.order.volume").AddDatabase("order-db-aspire");

var orderApi = builder.AddProject<Projects.Course_Order_API>("course-order-api");
orderApi.WithReference(sqlserverOrderDB).WaitFor(sqlserverOrderDB).WithReference(rabbitmq).WaitFor(rabbitmq).WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion


var paymentApi = builder.AddProject<Projects.Course_Payment_API>("course-payment-api").WithReference(rabbitmq).WaitFor(rabbitmq).WithReference(keycloakEndpoint).WaitFor(keycloak); ;

var fileApi = builder.AddProject<Projects.Course_File_API>("course-file-api").WithReference(rabbitmq).WaitFor(rabbitmq).WithReference(keycloakEndpoint).WaitFor(keycloak);

var gatewayApi = builder.AddProject<Projects.Course_Gateway>("course-gateway").WithReference(keycloakEndpoint).WaitFor(keycloak);



#region WEB

var web = builder.AddProject<Projects.Course_Web>("course-web");
web.WithReference(catalogApi).WithReference(basketApi).WithReference(discountApi).
    WithReference(orderApi).WithReference(paymentApi).WithReference(fileApi).WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion



builder.Build().Run();
