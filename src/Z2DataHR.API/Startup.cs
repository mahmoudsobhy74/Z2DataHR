using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Z2DataHR.API.Extensions.Middleware;
using Z2DataHR.Application.Handlers;
using Z2DataHR.Application.Mappers;
using Z2DataHR.Application.Services;
using Z2DataHR.Domain.Tasks;
using Z2DataHR.Domain.Tasks.Commands;
using Z2DataHR.Domain.Tasks.Events;
using Z2DataHR.Infrastructure.Factories;
using Z2DataHR.Infrastructure.Repositories;
using FluentMediator;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Z2DataHR.Application.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Z2DataHR.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<ITaskService, TaskService>();
            services.AddTransient<ITaskRepository, TaskRepository>(); //just as an example, you may use it as .AddScoped
            services.AddSingleton<TaskViewModelMapper>();
            services.AddTransient<ITaskFactory, EntityFactory>();

            services.AddScoped<ICandidate, CandidateService>();
            services.AddScoped<IGeneralTypes, GeneralTypesService>();
            services.AddScoped<IInterviewer, InterviewerService>();

            services.AddScoped<IInterviews, InterviewsService>();

            services.AddScoped<IDepartmentPlan, DepartmentPlansService>();
            services.AddScoped<IEmployees, EmployeesService>();
            services.AddScoped<ITypes, TypesService>();

            //services.Configure<FormOptions>(o => {
            //    o.ValueLengthLimit = int.MaxValue;
            //    o.MultipartBodyLengthLimit = int.MaxValue;
            //    o.MemoryBufferThreshold = int.MaxValue;
            //});

            services.Configure<FormOptions>(o =>  // currently all set to max, configure it to your needs!
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
                o.ValueLengthLimit = 5000; // Limit on individual form values
                o.MultipartBodyLengthLimit = 737280000; // Limit on form body size
                o.MultipartHeadersLengthLimit = 737280000; // Limit on form header size
               
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 837280000; // Limit on request body size
            });

            services.Configure<DbConnectionString>(Configuration.GetSection("ConnectionStrings"));

            //Enable Cors

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });




            services.AddScoped<TaskCommandHandler>();
            services.AddScoped<TaskEventHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddFluentMediator(builder =>
            {
                builder.On<CreateNewTaskCommand>().PipelineAsync().Return<Domain.Tasks.Task, TaskCommandHandler>((handler, request) => handler.HandleNewTask(request));

                builder.On<TaskCreatedEvent>().PipelineAsync().Call<TaskEventHandler>((handler, request) => handler.HandleTaskCreatedEvent(request));

                builder.On<DeleteTaskCommand>().PipelineAsync().Call<TaskCommandHandler>((handler, request) => handler.HandleDeleteTask(request));

                builder.On<TaskDeletedEvent>().PipelineAsync().Call<TaskEventHandler>((handler, request) => handler.HandleTaskDeletedEvent(request));
            });

            services.AddSingleton(serviceProvider =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            Log.Logger = new LoggerConfiguration().CreateLogger();

            services.AddOpenTracing();

            services.AddOptions();

            services.AddMvc();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files"

            });

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                await next.Invoke();
            });



            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Tasks API V1");
            });
        }
    }
}
