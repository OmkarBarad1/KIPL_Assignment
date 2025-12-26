using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileRead;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.Fileupload;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddScoped<MatrixUploadService>();

builder.Services.AddSingleton<IMatrixParser, TxtMatrixParser>();
builder.Services.AddSingleton<IMatrixParser, JsonMatrixParser>();




var dataFilePath = builder.Configuration.GetValue<string>("DataFilePath")
    ?? Path.Combine(builder.Environment.ContentRootPath, "Data", "matrix.txt");


builder.Services.AddSingleton<IRepository, Repository>();

builder.Services.AddScoped<IMachineAssetServices>(sp =>
{
    var _repository = sp.GetRequiredService<IRepository>();
    return new MachineAssetServices(_repository, dataFilePath);
});


builder.Services.AddSingleton(_ => new MatrixWriter(dataFilePath));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
