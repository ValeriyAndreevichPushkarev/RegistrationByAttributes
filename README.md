# Automatic registration by attributes for .net

That repository contains utilities to register any interface implementations in the container.

All you need is to add an attribute to the interface and call one method.

Now the project supports Unity and DependencyInjection.Abstractions.

No more problems with importing types from other assemblies and so on. By design :).

Just add attributes.

## How to use it?

Add a **TypeRegistrationAttribute** to the interface whose implementations you want to register in the container.

Add **DerivedTypeRegistrationAttribute** to the implementation to override LifetimeManagement or specify Name.

Specify **LifetimeManagementType**.

Specify **Name** if you want to make Keyed registration.

Call **(YourContainer).RegisterWithAttributes()**.

Call **(YourContainer).RegisterWithAttributes(AnotherAssembly)** to register types with registration with attributes from another assembly.

## How to add your container?

Make your own **CommonRegistration**.

override 3 methods - **registerInContainer**, **registerManyInContainer**.

Write your own mapping for **LifetimeManagementType**.
