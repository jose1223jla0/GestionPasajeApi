using gestionApi.Repository;
using gestionApi.Repository.Interface;
using gestionApi.Services;
using gestionApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
/*==========================================================================================
 Inicio de area de servicio
===========================================================================================  */
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")?.Split(",");

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        if (origenesPermitidos != null)
        {
            politica.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
        }
    });
});

builder.Services.AddHttpClient(); //api externos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConductorRepositorio, ConductorRepositorio>();
builder.Services.AddTransient<IPasajeroRepositorio, PasajeroRepositorio>();
builder.Services.AddTransient<IPasajeroService, PasajeroService>();
builder.Services.AddTransient<IHorarioRepositorio, HorarioRepositorio>();
builder.Services.AddTransient<IRutaReposiotorio, RutaRepositorio>();
builder.Services.AddTransient<IVehiculoRepositorio, VehiculoRepositorio>();
builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();

/*==========================================================================================
 fin de area de servicio
===========================================================================================  */
var app = builder.Build();

/*==========================================================================================
 Inicio de area de middleware
===========================================================================================  */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();
app.UseHttpsRedirection();

/*==========================================================================================
 fin de area de middleware
===========================================================================================  */
app.Run();