using ModelPlaneInfo.Data;
using ModelPlaneInfo.IO;
using ModelPlaneInfo.Repositories;
using ModelPlaneInfo.Repositories.Interfaces;
using System.Web;
using System.Web.Hosting;

namespace ModelPlaneInfo.Web.Infrastructure
{
    public class UoWCreator
    {
        private static DataContext dataContext;
        private static IInfoUnitOfWork uow;

        public static IInfoUnitOfWork GetInstance()
        {
            if (dataContext == null)
            {
                string virtualPath = "~" 
                    + HttpContext.Current
                    .Application["dataFilesPath"]
                    as string;
                string path = HostingEnvironment
                    .MapPath(virtualPath);
                dataContext = 
                    new DataContext(path)
                {
                    FileIoController = 
                    new BinarySerializationController()
                };
                dataContext.CreateTestData();
                uow = new FileBasedUnitOfWork(dataContext);
            }
            return uow;
        }
    }
}