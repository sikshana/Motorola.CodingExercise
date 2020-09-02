using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Motoroal.CodingExercise.Repository
{
    public sealed class RepoDependencyModeule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repo", StringComparison.Ordinal)).AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
