##Dependency Injection with Unity Cheat Sheet

###What is Unity
>Unity is a lightweight, extensible dependency injection container that supports `interception`, `constructor injection`, `property injection`, and `method call injection`. You can use Unity in a variety of different ways to help decouple the components of your applications, to maximize coherence in components, and to simplify design, implementation, testing, and administration of these applications.
####Features


Patterns

- **Composition Root** [http://blog.ploeh.dk/2011/07/28/CompositionRoot/](http://blog.ploeh.dk/2011/07/28/CompositionRoot/)
>A Composition Root is a (preferably) unique location in an application where modules are composed together.


- **Inversion of Control (IoC) pattern.**
> This generic pattern describes techniques for supporting a plug-in architecture where objects can look up instances of other objects they require.
> 

- **Dependency Injection (DI) pattern.**

>This is a special case of the IoC pattern and is an interface programming technique based on altering class behavior without the changing the class internals. Developers code against an interface for the class and use a container that injects dependent object instances into the class based on the interface or object type. The techniques for injecting object instances are interface injection, constructor injection, property (setter) injection, and method call injection.

- **Interception pattern**. 

> This pattern introduces another level of indirection. This technique places a proxy object between the client and the real object. The client behavior is the same as when interacting directly to the real object, but the proxy intercepts the calls and manages their execution by collaborating with the real object and other objects as required.



####Terms

- `Container` - is the main object used to create objects and inject dependencies into them


#### When to Use Unity



####Facts
- You can configure type mappings either using a configuration file or in code
 -You wish to build your application according to sound object oriented principles (following the five principles of class design, or SOLID), but doing so would result in large amounts of difficult-to-maintain code to connect objects together.
- Your objects and classes may have dependencies on other objects or classes.
- Your dependencies are complex or require abstraction.
- You want to take advantage of constructor, method, or property call injection features.
- You want to manage the lifetime of object instances.
- You want to be able to configure and change dependencies at run time.
- You want to intercept calls to methods or properties to generate a policy chain or pipeline containing handlers that implement crosscutting tasks.
You want to be able to cache or persist the dependencies across post backs in a web application.

####Types of Injection
---
- Property Setter Injection

#### Lifetime Management

----------
The Unity container manages the creation and resolution of objects based on a lifetime you specify when you register the type of an existing object, and uses the default lifetime if you do not specify a lifetime manager for your type registration.
>The default behavior is for the container to use a **transient lifetime manager**. It creates a new instance of the registered, mapped, or requested type each time you call the Resolve or ResolveAll method or when the dependency mechanism injects instances into other classes

- **TransientLifetimeManager**

For this lifetime manager Unity creates and returns a new instance of the requested type for each call to the Resolve or ResolveAll method. This lifetime manager is used by default for all types registered using the RegisterType, method unless you specify a different lifetime manager.

- **ContainerControlledLifetimeManager** 

For this lifetime manager Unity returns the same instance of the registered type or object each time you call the Resolve or ResolveAll method or when the dependency mechanism injects instances into other classes. This lifetime manager effectively implements a singleton behavior for objects.

- **HierarchicalLifetimeManager**

For this lifetime manager, as for the ContainerControlledLifetimeManager, Unity returns the same instance of the registered type or object each time you call the Resolve or ResolveAll method or when the dependency mechanism injects instances into other classes. The distinction is that when there are child containers, each child resolves its own instance of the object and does not share one with the parent. 

- **PerResolveLifetimeManager**

For this lifetime manager the behavior is like a TransientLifetimeManager, but also provides a signal to the default build plan, marking the type so that instances are reused across the build-up object graph. In the case of recursion, the singleton behavior applies where the object has been registered with the PerResolveLifetimeManager.


- **PerThreadLifetimeManager**

For this lifetime manager Unity returns, on a per-thread basis, the same instance of the registered type or object each time you call the Resolve or ResolveAll method or when the dependency mechanism injects instances into other classes. This lifetime manager effectively implements a singleton behavior for objects on a per-thread basis. PerThreadLifetimeManager returns different objects from the container for each thread.

- **PerRequestLifetimeManager**

The PerRequestLifetimeManager class enables the container to create new instances of registered types for each HTTP request in an ASP.NET MVC application or an ASP.NET Web API application. Each call to Resolve a type within the context of a single HTTP request will return the same instance: in effect, the Unity container creates singletons for registered types for the duration of the HTTP request.

To use this lifetime manager in an ASP.NET MVC application, you must add the Unity bootstrapper for ASP.NET MVC NuGet package to your project. This package adds the following source files to your project along with the DLL and references.

- `UnityConfig.cs`. 

- `UnityMvcActivator.cs`. 

- ExternallyControlledLifetimeManager

This lifetime manager allows you to register type mappings and existing objects with the container so that it maintains only a weak reference to the objects it creates when you call the Resolve or ResolveAll method or when the dependency mechanism injects instances into other classes based on attributes or constructor parameters within that class. This allows other code to maintain the object in memory or dispose it and enables you to maintain control of the lifetime of existing objects or allow some other mechanism to control the lifetime.

 Using the ExternallyControlledLifetimeManager enables you to create your own custom lifetime managers for specific scenarios.


####Configuring Unity In a Console application

----------



####Configuring Unity in Web API project


####Configuring Unity in a ASP.NET MVC project

####Configuring type mappings using a configuration file

####Configuring type mappings in code 