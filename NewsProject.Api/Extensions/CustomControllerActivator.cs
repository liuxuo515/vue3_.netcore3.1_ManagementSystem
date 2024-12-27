using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NewsProject.Api.Extensions
{
    public class CustomControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            // 从服务提供者获取控制器实例
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();
            var serviceProvider = context.HttpContext.RequestServices;

            var controllerInstance = ActivatorUtilities.CreateInstance(serviceProvider, controllerType);

            if (controllerInstance == null)
            {
                throw new InvalidOperationException($"Failed to create controller instance for type {controllerType.FullName}.");
            }

            return controllerInstance;
        }

        public void Release(ControllerContext context, object controller)
        {
            // 释放控制器（如果需要）
            if (controller is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
