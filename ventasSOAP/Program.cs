using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using ventasSOAP;

var builder = WebApplication.CreateBuilder(args);

// Habilitar soporte para WCF/SOAP
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, ServiceBehaviorAttribute>(v => new ServiceBehaviorAttribute
{
    IncludeExceptionDetailInFaults = true // Para ver errores si fallamos
});

var app = builder.Build();

// Configurar el endpoint SOAP
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<ServicioVentas>();
    
    // Aquí definimos la ruta donde escuchará el servicio: /Service.svc
    serviceBuilder.AddServiceEndpoint<ServicioVentas, IFACTURA>(new BasicHttpBinding(), "/Service.svc");
    
    // Habilitar el WSDL (para que otros programas sepan cómo usar tu servicio)
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpGetEnabled = true;
});

app.Run();