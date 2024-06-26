using CarvedRock_WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options
    .AddDefaultPolicy(builder =>
            builder
                //.AllowAnyOrigin()
                .WithOrigins("https://localhost:7220", "http://localhost:7220", "https://broodjes.sintmaartencampus.be", "http://broodjes.sintmaartencampus.be")
                .AllowAnyHeader()
                .AllowAnyMethod()
                //.AllowCredentials()
                );
    //.AddPolicy("OpenCorsPolicy", builder =>
    //    builder
    //        .WithOrigins("https://localhost:7220", "http://localhost:7220", "https://broodjes.sintmaartencampus.be", "http://broodjes.sintmaartencampus.be")
    //        .AllowAnyHeader()
    //        .AllowAnyMethod());
});

//app.UseCors(b => {
//    b.WithOrigins("https://localhost:7220");
//    b.AllowAnyHeader();
//    b.AllowAnyMethod();
//});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseCors("OpenCorsPolicy");
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
