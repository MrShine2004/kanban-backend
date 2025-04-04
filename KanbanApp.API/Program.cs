using KanbanApp.Application.Services;
using KanbanApp.Core.Abstractions.IBoards;
using KanbanApp.Core.Abstractions.IUsers;
using KanbanApp.DataAccess;
using KanbanApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KanbanAppDbContext>(
	options =>
	{
		options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(KanbanAppDbContext)));
	});

builder.Services.AddScoped<IBoardsKanbanService, BoardsKanbanService>();
builder.Services.AddScoped<IColumnsKanbanService, ColumnsKanbanService>();
builder.Services.AddScoped<ITasksKanbanService, TasksKanbanService>();
builder.Services.AddScoped<IUsersKanbanService, UsersService>();
builder.Services.AddScoped<ISubtasksKanbanService, SubtasksKanbanService>();


builder.Services.AddScoped<IBoardsKanbanRepository, BoardKanbanRepository>();
builder.Services.AddScoped<IColumnsKanbanRepository, ColumnKanbanRepository>();
builder.Services.AddScoped<ITasksKanbanRepository, TaskKanbanRepository>();
builder.Services.AddScoped<IUsersKanbanRepository, UserRepository>();
builder.Services.AddScoped<ISubtasksKanbanRepository, SubtaskKanbanRepository>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(
        "http://localhost:3000",
        "https://kanbanapp-64oax09ov-alexs-projects-136f645d.vercel.app",
        "https://karibanapp-alexe-projects-136f645dcvercol.app"
    )
    .AllowCredentials()
);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
