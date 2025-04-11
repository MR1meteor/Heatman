using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.DependencyInjection.Interfaces;

namespace Shared.DependencyInjection;

#nullable disable
public static class DIContainer
{
    public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly mainAssembly)
    {
        var commonType = typeof(T);
        foreach (var assembly in GetAssemblies(mainAssembly).ToList())
        foreach (var type in assembly.GetExportedTypes()
                     .Where((Func<Type, bool>)(x => x.IsClass && !x.IsAbstract && commonType.IsAssignableFrom(x)))
                     .ToList())
        {
            var serviceType = type.GetInterfaces()
                .FirstOrDefault((Func<Type, bool>)(x => commonType.IsAssignableFrom(x)));
            if (serviceType == null || !serviceType.IsInterface)
                throw new ApplicationException("Interface doesn't exist");
            if (serviceType.ContainsGenericParameters)
            {
                var genericTypeDefinition1 = serviceType.GetGenericTypeDefinition();
                var genericTypeDefinition2 = type.GetGenericTypeDefinition();
                services.Add(new ServiceDescriptor(genericTypeDefinition1, genericTypeDefinition2,
                    GetLifetime(genericTypeDefinition2)));
            }
            else
            {
                services.Add(new ServiceDescriptor(serviceType, type, GetLifetime(type)));
            }
        }
    }

    private static IEnumerable<Assembly> GetAssemblies(Assembly mainAsm)
    {
        var list = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var loaded = new List<Assembly>
        {
            mainAsm
        };
        var predicate = (Func<Assembly, bool>)(x => x.FullName.StartsWith("Heatman") && !loaded.Contains(x));
        var collection = list.Where(predicate);
        loaded.AddRange(collection);
        var assemblyStack = new Stack<Assembly>(new Assembly[1]
        {
            mainAsm
        });
        do
        {
            foreach (var assemblyName in assemblyStack.Pop().GetReferencedAssemblies()
                         .Where((Func<AssemblyName, bool>)(a => a.FullName.StartsWith("Heatman"))))
            {
                var reference = assemblyName;
                var assembly =
                    loaded.FirstOrDefault((Func<Assembly, bool>)(x => x.GetName().FullName.Equals(reference.FullName)));
                if (assembly == null)
                    assembly = AppDomain.CurrentDomain.Load(reference);
                if (assembly != null)
                {
                    assemblyStack.Push(assembly);
                    if (!loaded.Exists((Predicate<Assembly>)(x => x.Equals(assembly))))
                        loaded.Add(assembly);
                }
            }
        } while (assemblyStack.Count > 0);

        return WithLooseCouplingAssemblies(loaded);
    }

    private static ServiceLifetime GetLifetime(Type type)
    {
        if (typeof(ISingleton).IsAssignableFrom(type))
            return ServiceLifetime.Singleton;
        if (typeof(ITransient).IsAssignableFrom(type))
            return ServiceLifetime.Transient;
        if (typeof(IScoped).IsAssignableFrom(type))
            return ServiceLifetime.Scoped;
        throw new ApplicationException("Wrong dependency");
    }

    private static List<Assembly> WithLooseCouplingAssemblies(List<Assembly> loaded)
    {
        var location = Assembly.GetEntryAssembly()?.Location;
        var files = Directory.GetFiles(Path.GetDirectoryName(location), "Heatman.*.dll");
        var assemblyList = new List<Assembly>();
        if (string.IsNullOrEmpty(location))
            return null;
        foreach (var str in files)
        {
            var filename = str.Split(Path.DirectorySeparatorChar).LastOrDefault();
            if (!loaded.Any((Func<Assembly, bool>)(l => l.FullName.Contains(filename.Split(".dll").FirstOrDefault()))))
            {
                var assembly = AppDomain.CurrentDomain.Load(
                    File.ReadAllBytes(Path.GetDirectoryName(location) + Path.DirectorySeparatorChar + filename));
                assemblyList.Add(assembly);
            }
        }

        assemblyList.AddRange(loaded);
        return assemblyList;
    }
}