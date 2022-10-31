// using Furion;
// using MentalIsland.AllDependency;
// using MentalIsland.Core.CodeFirst.Identity.Models;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.DependencyInjection;

// namespace MentalIsland.Migrations.Extensions;

// public static class AutoDependency
// {
//     /// <summary>
//     /// 自动注入IDependency
//     /// </summary>
//     /// <param name="serviceCollection"></param>
//     /// <returns></returns>
//     public static IServiceCollection AddAutoDependency(this IServiceCollection serviceCollection)
//     {
//         foreach (var assembly in App.Assemblies)
//         {
//             var types = assembly.GetTypes().Where(a => typeof(IDependency).IsAssignableFrom(a) && !a.IsAbstract).ToList();
//             if (types.Count <= 0) continue;
//             foreach (var type in types)
//             {
//                 if (type == null) continue;
//                 var interfacer = type.GetInterfaces().FirstOrDefault();
//                 if (interfacer != null)
//                     if (typeof(IScopeDependency).IsAssignableFrom(type))
//                         serviceCollection.AddScoped(interfacer, type);
//                     else if (typeof(ISingleDependency).IsAssignableFrom(type))
//                         serviceCollection.AddSingleton(interfacer, type);
//                     else if (typeof(ITransientDependency).IsAssignableFrom(type))
//                         serviceCollection.AddTransient(interfacer, type);
//                     else throw new ArgumentOutOfRangeException();
//             }
//         }

//         return serviceCollection;
//     }

// }