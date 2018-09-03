using Agatha.ServiceLayer;
using AgathaSample.Common.RequestsResponses;
using AgathaSample.ServiceLayer.Handlers;

namespace AgathaSample.ServiceLayer
{
    public static class ComponentRegistration
    {
        public static void Register()
        {
            var configuration = new ServiceLayerConfiguration(typeof(GetConsultantsHandler).Assembly, 
                                                              typeof(GetConsultantsRequest).Assembly,
                                                              typeof(Agatha.Ninject.Container));
            configuration.Initialize();
        }
    }
}
