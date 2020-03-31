
using Cw3.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Cw3
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
            services.AddSingleton<IDbService, MockDbService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //
            app.UseDeveloperExceptionPage();

            app.UseRouting();//jezeli nie jest na produkcji

            //////////////////////////////////////////
            //   app.UseHttpsRedirection();//������������� ����� ���� �������� �� https 
            //�� �������� � ���� ��������

            //������� �� ������ �������� http
            app.Use(async (contex, c)=>{
                contex.Response.Headers.Add("Secret", "123");
                await c.Invoke();//��������� ������ � ���� ��������
            });
            ///////////////////////////////////

            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
