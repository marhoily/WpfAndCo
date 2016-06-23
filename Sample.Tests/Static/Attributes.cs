using System;
using JetBrains.Annotations;

namespace Sample
{
    [MeansImplicitUse(ImplicitUseTargetFlags.Members)]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CqrsEventAttribute : Attribute{}
    [MeansImplicitUse(ImplicitUseTargetFlags.Members)]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DtoAttribute : Attribute{}
    [MeansImplicitUse(ImplicitUseTargetFlags.Itself)]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IoCAttribute : Attribute{}
}