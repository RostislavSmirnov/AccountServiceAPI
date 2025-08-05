# 🏦 Bank Account Service API

**Микросервис для управления банковскими счетами**, разработанный в рамках стажировки.

---

## 📋 Описание

Сервис предоставляет REST API для выполнения основных операций с банковскими счетами:
- ✅ Создание счета
- 🔍 Получение информации о счете
- ❌ Удаление счета
- 🔁 Переводы между счетами

🔐 API защищено с помощью **JWT-аутентификации через Keycloak**.  
📦 Проект полностью контейнеризирован с использованием **Docker и Docker Compose**.

---

## 🛠️ Технологический стек

- **Язык и платформа:** C#, .NET 9  
- **Архитектура:** REST, CQRS (с использованием MediatR)  
- **Валидация:** FluentValidation  
- **Аутентификация:** JWT, Keycloak  
- **Контейнеризация:** Docker, Docker Compose  

---

## ⚙️ Предварительные требования

Перед запуском убедитесь, что установлены:
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

---

## 🚀 Быстрый старт: Запуск через Docker Compose

> Это основной и рекомендуемый способ запуска проекта. Он автоматически поднимает:
> - сам сервис
> - сервер аутентификации Keycloak

### 1. Клонируйте репозиторий

```bash
git clone https://github.com/RostislavSmirnov/AccountServiceAPI.git
cd AccountServiceAPI
```

### 2. Запустите сервисы

Выполните команду из корня проекта (где находится `docker-compose.yml`):

```bash
docker-compose up --build
```

> ⏳ При первом запуске будут загружены образы и собран проект. Это может занять несколько минут.

### 3. Дождитесь запуска

В терминале появятся строки:

```
bankaccount-api  | Application started. Press Ctrl+C to shut down.
keycloak         | Keycloak 22.0.5 on JVM ... started in Xs. Listening on: http://0.0.0.0:8080
```

---

## 🌐 Сервисы доступны по адресам:

- 🔧 **API (Swagger UI):** http://localhost:8000/swagger  
- 🔐 **Keycloak (Admin Console):** http://localhost:8080/admin

---

## 🔐 Работа с API (через Swagger)

Все эндпоинты **защищены**. Для доступа необходим **JWT-токен**.

### 1. Настройка Keycloak (один раз)

Перейдите в админку: [http://localhost:8080/admin](http://localhost:8080/admin)  
Войдите:

- **Username:** `admin`  
- **Password:** `admin`

#### Создайте Realm:
- Имя: `bank-realm`

#### Внутри него создайте Client:
- **Client ID:** `bank-account-api`  
- **Тип:** `public` (Client authentication OFF)  
- ✅ Включите: **Direct Access Grants**

#### Создайте пользователя:
- **Username:** `ivan_ivanov`  
- Задайте пароль: `12345`

---

### 2. Получение JWT-токена

POST-запрос на:

```
POST http://localhost:8080/realms/bank-realm/protocol/openid-connect/token
```

**Body (x-www-form-urlencoded):**
```
client_id: bank-account-api
username: ivan_ivanov
password: 12345
grant_type: password
scope: openid
```

📎 В ответе скопируйте значение из `access_token`.

---

### 3. Использование токена в Swagger

- Перейдите на [http://localhost:8000/swagger](http://localhost:8000/swagger)
- Нажмите **"Authorize"** (вверху справа)
- Вставьте:

```
Bearer <ВАШ_ТОКЕН>
```

Теперь можно выполнять запросы к защищённым эндпоинтам ✅

---

## 🐞 Отладка в Visual Studio

Проект поддерживает запуск и отладку внутри Docker-контейнера из **Visual Studio 2022**:

1. Откройте `.sln` файл
2. Выберите конфигурацию запуска: `Container (Dockerfile)`
3. Нажмите `F5`

🌐 Браузер откроется по адресу: [http://localhost:8001/swagger](http://localhost:8001/swagger)

---

## 📎 Полезные ссылки

- [Docker Compose документация](https://docs.docker.com/compose/)
- [Keycloak документация](https://www.keycloak.org/documentation)
- [MediatR на GitHub](https://github.com/jbogard/MediatR)

---

## 🧑‍💻 Автор

Проект разработан в рамках стажировки — [Rostislav Smirnov](https://github.com/RostislavSmirnov)


