AccountServiceAPI – REST API для управления банковскими счетами
Account Service — это микросервис, реализующий REST API для управления банковскими счетами. Построен на NET 9, CQRS.

Стек технологий
Категория	Технология
Backend	.NET 9, ASP.NET Core
Архитектура	Vertical Slice Architecture, CQRS, MediatR
Валидация	FluentValidation
База данных	In-Memory (заглушки репозиториев), куда я вписал несколько объектов изначально, для тестирования
Документация	Swagger (OpenAPI)
CI/CD	GitHub Actions

Архитектура
Vertical Slice Architecture
Это мой первый проект, который построен на архитектуре Vertical Slice, где весь код, относящийся к одной бизнес-логике, расположен в одной папке, от чего логика возможно получилась не совсем интуитивной, но на мой взгляд соответсвтует стандартам. Суть этой логике в низкой взаимосвязанности, что упрощает поддержку кода, и позволяет более комфортно реализовывать микросервисную архитектуру.

CQRS с MediatR
Паттерн CQRS (Command Query Responsibility Segregation) реализован через MediatR:

Commands — операции, изменяющие состояние (создание счёта, перевод).

Queries — операции только для чтения (получение счёта, выписки).

Handlers — обработчики команд и запросов, содержащие бизнес-логику, по сути как Service в трехслойной архитектуре, но локальный.

Controllers — направляют команды и запросы в MediatR.

Валидация и обработка ошибок
FluentValidation — описание правил и валидации.

MediatR Pipeline Behaviors — валидация встроена в пайплайн.

ErrorHandler Middleware — глобальный перехват ошибок и возврат стандартизированных ответов.

Инфраструктура и заглушки
Все внешние зависимости вынесены в слой Infrastructure через интерфейсы, что обеспечивает слабую связность и легкость тестирования.

Реализованные In-Memory заглушки:
IAccountRepository — репозиторий банковских счетов

ICustomerVerificationService — верификация клиентов

ICurrencyService — проверка валют

Я первый раз делал API используя CQRS с MediatR, при разработке я частично опирался на другие работы, я использовал информацию из следующих источников:
https://amit-naik.medium.com/building-scalable-apis-with-vertical-slice-architecture-in-net-23f9d8c8a84e

https://github.com/nadirbad/VerticalSliceArchitecture?

https://codewithmukesh.com/blog/cqrs-and-mediatr-in-aspnet-core

[https://medium.com/%40mmudarrib2807/](https://medium.com/@mmudarrib2807/mastering-vertical-slice-architecture-in-net-core-a-clean-scalable-approach-to-web-api-design-6f6813488d17)

