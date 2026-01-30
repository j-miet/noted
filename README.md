# --Work In Progress--

## Idea

A [Milanote](https://milanote.com/)-like fullstack application.

- a simplified version of original app, meaning it will lack many features
- focus on **<u>local</u>** use (= localhost). Maybe deployed later, but that depends on how much the project scales and gets polished
- base stack (probably add/change stuff later):
  - **Front:** React (Vite) + TypeScript, React Router, Zustand (states/stores). Vanilla CSS only -> no Tailwind or
    similar.
  - **Back:** ASP.NET Core
  - **Data:** sqlite initially, test also PostgreSQL later. EF Core for ORM.
  - **Other:**
    - _testing:_ Jest & Supertest (front), xUnit (back)
    - _linting:_ ESLint,

## Features

- User can create canvas pages
- Each page has a canvas area where user can add rectangular note blocks.
  - text can be freely inserted into a note by clicking on it and editing contents
  - notes can be dragged around the canvas with mouse (drag & drop)
  - notes will automatically save contents + location quickly after user stops editing or dragging them.
    However this might require some compromising such as limiting saving process to manual "Save" button or at least
    adding some kind of timer before saving, otherwise server is flooded with position updating requests...
  - can lock notes in place
  - canvas has limited area
- basic CRUD operations using REST Api http requests
- routing so users can easily go back forth between canvases

**Extras to be added after app is in a working state and has basic features implemented:**

- create reference notes to other canvases
  - use contents from another note. Not limited to current canvas so it could fetch data from other canvases too
  - create a link to another canvas. Clicking this takes you to another canvas page
- create different types of notes
  - lists
  - line links i.e. connect two notes with a line/arrow and insert text above it
  - to-do lists
  - visual elements such as diagrams/tables.
    - Images also for local use. Store them in a directory, save image path/identifier string into database, then
      simply load image using this path/ID. Avoids the hassle of saving images to database which is a bad idea.
    - _if app will eventually get deployed_ then similar idea works. Images are now stored on a web database server
      which is specialized storing image data, then fetched from there using some ID.
- canvas can be expanded to fit more stuff
- different themes/color schemes (dark mode, light mode, custom)
- import/export data using JSON format
- simple breadcrumbs: keep short list of recently visited canvas pages and allow easily jumping between them
- users: register/login, authentication, roles (authorization), web tokens (localStorage first -> Cookies later)
- ...and probably more ideas to come while developing the app
