# Автоматическая регистрация по атрибутам для .net

Решил сделать отдельный проект. Добавил регистрацию множеств реализаций, или конкретных реализаций с управлением LifetimeManagement-ом (для Unity).

## Как пользоваться?

Добавить **TypeRegistrationAttribute** на базовую сущность, реализации которой хочется регистрировать в контейнере.

Добавить **DerivedTypeRegistrationAttribute** на реализацию чтобы переопределить LifetimeManagement.

Указать **LifetimeManagementType**.

Вызвать **UnityCommonRegistration.Register(container)**.

## Как добавить свой контейнер?

переопределить 2 метода - **registerInContainer**, **registerManyInContainer**. 

Написать свой маппинг для **LifetimeManagementType**.
