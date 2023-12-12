# API plugin for nopCommerce

This plugin provides a RESTful API for managing resources in nopCommerce.

## What is a RESTful API?


HTTP requests are often the way that you interact with a RESTful API.
A client makes an HTTP request to a server and the server responds with an HTTP response.

In a HTTP request, you need to define the type of action that you want to perform against a resource. There are four primary actions associated with any HTTP request (commonly referred to as CRUD):

**POST** (Create)

**GET** (Retrieve)

**PUT** (Update)

**DELETE** (Delete)

A resource is a data object that can be accessed via an HTTP request. The API allows you to “access your nopCommerce site’s data (resources) through an easy-to-use HTTP REST API”. In the case of the most recent version of the API (nopCommerce version 3.80), the resources include the following nopCommerce objects:

[**Products**](Products.md)

With the nopCommerce API, you can perform any of the four CRUD actions against any of your nopCommerce site’s resources listed above. For example, you can use the API to create a product, retrieve a product, update a product or delete a product associated with your nopCommerce website.

## What about security?

The API plugin currently supports OAuth 2.0 Authorization Code grant type flow. So in order to access the resource endpoints you need to provide a valid AccessToken.

### Get the token
GET /token
```x-www-form-urlencoded

    username={{YOUR_EMAIL}}
    password={{YOUR_PASSWORD}}
```

<details><summary>Response</summary><p>
```json
	HTTP/1.1 200 OK

{
	"access_token": "eyJuYmYiOiIxNjMzMDIzOTcyIiwiZXhwIjoiMTYzMzI4MzE3MiIsImh0dHA6Ly9zY2hlbWFzLnhtbMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGFkbWluLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiN2UyNDhlMGYtOTVhMi00YTYwLTgxY2MtMjRiYzM5NzUxMDUyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFkbWluQGFkbWluLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJBZG1pbmlz",
    "token_type": "Bearer",
    "expires_in": 6943283172,
    "error_description": null
}
```
</details>