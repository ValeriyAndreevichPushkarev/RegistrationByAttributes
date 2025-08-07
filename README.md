# Automatic registration by attributes for .net

Decided to make a separate project. Added registration of multiple implementations, or specific implementations with LifetimeManagement management (for Unity).

The fun is that this approach **covers all work with container**.

Also, if you need to register types from any other assembly with registrations with attributes — simply call **RegisterFromAnotherAssembly**.

If you want to use different implementations depend on build type - use preprocessor directives.

No more problems with importing types from another assemblies and so on. By design :).

Just add attributes.

## How to use?

Add a **TypeRegistrationAttribute** to the base entity whose implementations you want to register in the container.

Add **DerivedTypeRegistrationAttribute** to the implementation to override LifetimeManagement.

Specify **LifetimeManagementType**.

Call **(YourContainer).RegisterWithAttributes()**.

Call **(YourContainer).RegisterWithAttributes(AnotherAssembly)** to register types with registration with attributes from another assembly.

## How to add your container?

Made your own **CommonRegistration**.

override 3 methods - **registerInContainer**, **registerManyInContainer**.

Write your own mapping for **LifetimeManagementType**.

