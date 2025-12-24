using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.Fileupload;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddScoped<MatrixUploadService>();

builder.Services.AddScoped<IUploadedMatrixParser, TxtMatrixParser>();
builder.Services.AddScoped<IUploadedMatrixParser, JsonMatrixParser>();


var dataFilePath = builder.Configuration.GetValue<string>("DataFilePath")
    ?? Path.Combine(builder.Environment.ContentRootPath, "Data", "matrix.txt");


builder.Services.AddSingleton<IMachineAssetServices>(_ =>  new MachineAssetServices(dataFilePath));

builder.Services.AddSingleton<MatrixWriter>(_ => new MatrixWriter(dataFilePath));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
