//using QuorumCodingChallengeLegislativeData.Ioc;
using QuorumCodingChallengeLegislativeData.Api.Model;
using QuorumCodingChallengeLegislativeData.Api.Model.Interfaces;
using QuorumCodingChallengeLegislativeData.Api.Services;
using QuorumCodingChallengeLegislativeData.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IFile), typeof(BillFile));
builder.Services.AddScoped(typeof(IFile), typeof(LegislatorFile));
builder.Services.AddScoped(typeof(IFile), typeof(VoteFile));
builder.Services.AddScoped(typeof(IFile), typeof(VoteResultFile));
builder.Services.AddScoped(typeof(LegistorsService), typeof(LegistorsService));
builder.Services.AddScoped(typeof(BillsService), typeof(BillsService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
