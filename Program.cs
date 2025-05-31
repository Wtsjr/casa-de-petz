using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 💡 Adicionando suporte a controllers e Swagger
builder.Services.AddControllers();  // 🚀 Necessário para detectar os controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet API", Version = "v1" });
});

// 💡 🔥 Configurar CORS para permitir requisições do front-end
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()   // Permite qualquer origem
              .AllowAnyMethod()   // Permite qualquer método HTTP (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // Permite qualquer cabeçalho
    });
});

var app = builder.Build();

// 🚀 Configurar o Swagger corretamente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 💡 🔥 Aplicar a política de CORS antes de mapear os controllers
app.UseCors("PermitirTudo");

app.MapControllers();
app.Run();
