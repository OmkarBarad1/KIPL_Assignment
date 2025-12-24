

using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileCheck;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileParsers;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileReader;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileWriter;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.ParserResolver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IMatrixFileLocator, MatrixFileLocator>();
builder.Services.AddScoped<MatrixUploadService>();
builder.Services.AddScoped<MatrixParserResolver>();
builder.Services.AddScoped<MatrixWriter>();
builder.Services.AddScoped<IUploadedMatrixParser, TxtMatrixParser>();
builder.Services.AddScoped<IUploadedMatrixParser, JsonMatrixParser>();
builder.Services.AddScoped<GetAssetByMachineNameUseCase>();
builder.Services.AddScoped<GetMachineByAssetNameUseCase>();
builder.Services.AddScoped<GetMachineThatUseLatestSeriesOfAssetUseCase>();
builder.Services.AddScoped<IDataSource, TxtDataSource>();
builder.Services.AddScoped<IMatrixFileLocator, MatrixFileLocator>();
builder.Services.AddScoped<MatrixParserResolver>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
