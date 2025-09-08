using System;
using Application;
using Domain.Models;
using MessagePipe;
using Repositories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
public class Installer : LifetimeScope
{
    [SerializeField] private ConfigRepository _configRepository;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        builder.RegisterInstance(_configRepository);
        builder.Register<GridModel>(Lifetime.Singleton);
        builder.Register<BuildingModelFactory>(Lifetime.Singleton);
        builder.Register<PlaceBuildingUseCase>(Lifetime.Singleton).As<IInitializable>().As<IDisposable>();
        builder.Register<BuyBuildingUseCase>(Lifetime.Singleton).As<IInitializable>().As<IDisposable>();
        builder.Register<RemoveBuildingUseCase>(Lifetime.Singleton).As<IInitializable>().As<IDisposable>();
        builder.Register<UpgradeBuildingUseCase>(Lifetime.Singleton).As<IInitializable>().As<IDisposable>();
        builder.Register<MoveBuildingUseCase>(Lifetime.Singleton).As<IInitializable>().As<IDisposable>();
        builder.Register<InitializeGridUseCase>(Lifetime.Singleton).As<IInitializable>();
    }
}
}
