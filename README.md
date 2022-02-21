# Media Tracker

Here's my demo - https://www.loom.com/share/725c3012282f4978b461120bf685e67b

![Screenshot of my project](https://github.com/AlexVitorCunha/MediaTracker/blob/master/COMP2084-Project-200465920/wwwroot/photos/screenshot.png?raw=true)

## Purpose

> This website was developed in my COMP2084 Server-Side Scripting-ASP.NET course at Georgian College. I was asked to create a C# .NET Core MVC web application that performs CRUD operations using a SQL Server database. This application helps users to track movies and tv shows they have watched or want to watch, read and write reviews from their favorite content and learn about new releases. The applications uses 4 tables detailed below.

## Tables used on the project
![Tables used on my project](https://github.com/AlexVitorCunha/COMP2084-Project-200465920/blob/master/COMP2084-Project-200465920/wwwroot/photos/tables.jpg)

## Technologies Learned

- ASP
- SQL
- Bootstrap 5
- C# .Net
- JavaScript

## Launch

To launch this application, you'll have to clone my Github repository, create your own database, update the appsetting.json file to update the database connection, and
add migration to create the tables on the database.

## Layout
> This application uses custom layout using [Bootswatch's](https://bootswatch.com/united/) personalized theme "united". Personalized fonts for the header obtained through [google fonts](https://fonts.google.com). Favicon generated from [Favicon](https://favicon.io/) and icon from [Flaticon](https://www.flaticon.com).

## Login
People can register and login to the application to be able to add media to their watchlist and write reviews. People can login using the application login or using their google account. There are 2 level of logins, Administrator and Customer, and each one of them has a different access level.

## Media Page
Any user can access the media page, see details and reviews about each of them. When they try to add a media to their watchlist they are redirected to the login page.

## Media Details
Anyone can access any media details, including the current reviews, but only logged in users are able to see and add their own reviews.

## Watchlist tab
Only registered users can access the watchlist tab to see what they added to their personal watchlist.

## Genres tab
The genres tab is only availabe for Administrator users to manage them. 


