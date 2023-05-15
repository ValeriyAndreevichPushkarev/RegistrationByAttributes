# Automatic registration by attributes for .net

Decided to make a separate project. Added registration of multiple implementations, or specific implementations with LifetimeManagement management (for Unity).

## How to use?

Add a **TypeRegistrationAttribute** to the base entity whose implementations you want to register in the container.

Add **DerivedTypeRegistrationAttribute** to the implementation to override LifetimeManagement.

Specify **LifetimeManagementType**.

Call **UnityCommonRegistration.Register(container)**.

## How to add your container?

Made your own **CommonRegistration**.

override 2 methods - **registerInContainer**, **registerManyInContainer**.

Write your own mapping for **LifetimeManagementType**.

