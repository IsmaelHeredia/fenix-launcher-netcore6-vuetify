# Fenix Launcher

It is a project made in Net Core 6, VueJS with Vuetify and MySQL, it is used to execute processes with tasks using system commands.

Processes and tasks can be conveniently recorded using a CRUD for both cases. Once the processes have been executed, the PIDs of each one are saved in the database so they can be closed at any time.

The system uses a username and password to protect the application, the routes are protected by validating a JWT that is saved in the user session, also in the profile section you can change the default data that is "supervisor" for the username and password.

Some images:

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEigZpvwCdDXXJcTerYuxq_36OR7w95NTFCP0DhDje-7vIGesn0OaJYXX2K1SGQBZkfsgXyua5zl1Y9135bMa_dcjpTzIwwVmp-XNwFxPt0XG_WRIRCMlGx6k1FQfVIicLo7DiCkkfSKq5uFfifZP20d1QqDSwC2Vb2s8mzvZiNgTE9Xswz3ZhxDVpHuFzA/s1887/1.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEi9b4vc6IVybc-sGCwlAfc_hVYBZycEbWWqE9-WGkQLDfZpPrhPUTqq9z-FKWwV4f6aOoMVHt2rqVltyGbYLA0RU2JoaskH4lDyF9jS9nHU0_DDpAuV7tb2f2T1jC8JWu5R9UqvICZxAU5UbnDMmoNoOQ49QJmwaWo31P0z2sgujSmGPEgs5jy90h1YH9c/s1897/2.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEigOkEUTZ2qYFkdCjhJSS7__xr6n5THq0U35ovZFvGFksC8_6RVpL0OhDPC_rRYkea0VWWJY9aTSRewC5SCnbBwCvIAaV_y5YBT4BzplVk7IXh_2XFgjY2eyrMl-5UXMdDYIqj_5u4-33irFeQ3O1Fok5BsVEFaYfIIUfbns_g4wUqG8F8xwMoGUKUUDDY/s1898/3.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEj5vFxqwdiWk-6zhzp_xeq2mhLL3ykBbpbbYk8Tzn94c2q3mUT2ADM9BYOfNdxpoJ3oCpXNq0UmRPxzRMWfx0cINpLHNvSTE38hDIngBTgGvDSgCrl7nAMswkerMwGpMHRA8dyP9EMrUXV04m1pp-O55WRCxUbZE6OIGpuMPFTeJRPD85gOWYffEvmCSmc/s1902/4.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEjghWhitORHetyctgZ3i1NJmbwWMSPxepfrNnIxpJJKJEeS9Gg2jAkiNrmKg15lmsVBmmkPHFlU4bw7PsXyZwAX6MrIjUCNq_6qdf7ZWEa8gi3Jhd_LKJYq9_XCytV0TGeI586iFQCBkBkP2uG7Eexht4W0NUCA7LNit_Qx5eqykC0kxYfjlxDK7z9RVkU/s1898/5.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgX3bo1GhksJxlDzPVJmECoyfDMhRgr-rfo4MneyO8abHBQ1_FzcHBlVW4Xbw8OEWCSPipTdEjDS_S4r-Z0uEBx-4pKuS9OyuNQuRe-JpinQO4Frjenxk1-2ON3jVNsqJ_Vrqs-1o3OljYTf1fwtgiXYy0H0HMFeOoPf7vZUzM5GWqBo036dSfSVxEGDkM/s1899/6.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEhRBQZz6L0F2bGsMUvgviHH0GvhC9iWly3nc7x42lcTs9weuIedoVOzNK-DevqK0S4ZYVCXAIOzPY2GVrMChV0CMMRDw-trwfQeRXe-XegCKPIkgD_zW8dANLFxIq9tug-KVpYjfw0VeElowj7BW6eIjgcJAMMo6oYKMlUumECzGUktPoMT00uQ-biVowQ/s1898/7.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEi2gNb9G-bymdXabCylXWEqHZ8yqeBAbecslJwvrijjAyIS8D6KVoUTfxkzJRo7-fKnByzX9-AIJ83ZGBynUqR0wPF_Tw6OS4ad4RhofNG3F-12EsYtN4y7anCveXvMVzawdSUA4aueel4v_t-gVPetI6oMMHglZJVXbSnW0E8E0VjqVmqUgQF3_JZH5F4/s1898/8.png)

![screenshot](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgYc0tLPNQBHRelSw-Es1ZBQNXcTKH4BEN8Z3laiaRHAzg7edUHoUARN3SZaz7Wm4m8G_aV0AtL3XNo3jcVheswIqvSXKUJdNurPBcH6kCyjYnAxJjibPbxOwHbAyV4B69WSSR6tWjfpEEWFwPeLaf6sQQ7y8bBegEQ6Fjev7b-B3_hXmuKhsGPwGrbVhQ/s1897/9.png)
