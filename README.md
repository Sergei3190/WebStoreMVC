WebStoreMVC

[![tests](https://github.com/Sergei3190/WebStoreMVC/actions/workflows/tests.yml/badge.svg)](https://github.com/Sergei3190/WebStoreMVC/actions/workflows/tests.yml)

Test web store.

Includes:

- administration area
- user account

Allows:

- sign in
- create orders from the cart
- view/create/edit/delete items, employees depending on the role assigned during user registration
- different search bots can retrieve an XML document with endpoints.
- move around the site using breadcrumbs

An SOA pattern was used in the implementation.

The client and backend have Docker support, the web store itself can be run in Docker Compose with a self-certified certificate.

This web store demonstrates the following skills:

- SOA pattern-based development 
- ASP.NET MVC (Razor)
- ASP.NET Web API using REST style
- ORM EF Core (Code First)
- MSSQL
- SQLite
- jQuery
- Ajax
- SignalR (chat alike)
- working with Identity
- Using cookies to store shopping carts,
- token substitution
- recreating system identifier
- customizing site navigation using breadcrumbs
- page dump
- creating TagHelpers and components
- Limiting access rights and hiding elements based on roles granted
- validation on the browser side, server side, hybrid
- Functional, system and unit testing (MsTest, xUnit using Moq)
- sitemap development
- log4net and serilog with SEQ logging
- application deployment in docker and docker compose
- CI/CD

----------------------------------------------------------------------------------------------------------------------------------

�������� ��� �������.

�������� � ����:

- ������� �����������������
- ������� ������������

���������:

- ����������������
- ����������� ������ �� �������
- �������������/���������/�������������/������� ������, ����������� � ����������� �� ����������� ��� ����������� ������������ ����
- ���������� ��������� ��������� ����� � ����� ����� ��� ��������� xml ��������� c ��������� �������
- ������������ �� ����� � ������� ������� ������

��� ���������� ������������� ������� SOA.

������ � ������ ����� ��������� Docker, ��� ��� ������� ����� ��������� � Docker Compose � �������������� ������������.

������ ��� ������� ������������� ��������� ������:

- ���������� �� ��������� �������� SOA 
- ASP.NET MVC (Razor)
- ASP.NET Web Api � ����������� ����� REST
- ORM EF Core (Code First)
- MSSQL
- Sqlite
- jQuery
- ajax
- SignalR (������� ����)
- ������ � �������� Identity
- ������������� ����� ��� �������� ������� �������,
- ������� ������
- ������������ �������������� �������
- ��������� ��������� ����� � ������� ������� ������
- ������������ �������� ������
- �������� TagHelpers � �����������
- ����������� ���� ������� � ������� ��������� �� ��������� �������� �����
- ��������� �� ������� ��������, �������, ���������
- ��������������, ��������� � ��������� ������������ (MsTest, xUnit � �������������� Moq)
- �������� ����� �����
- ������ � log4net � serilog � ������� ����� �� ������� SEQ
- ������������� ���������� � docker � docker compose
- CI/CD
