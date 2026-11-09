# RaceDay Part 3 MVC

This is the MVC front end for the RaceDay Part 2 API. It does not use EF Core or connect directly to SQL Server.

## Configure
Set `ApiSettings:BaseUrl` in appsettings.json to the actual Part 2 API address, with trailing slash. The uploaded Part 2 API uses session cookie `.RaceDay.Session` and these routes: `/api/auth/register`, `/api/auth/login`, `/api/auth/logout`, `/api/users/profile`, `/api/events`, `/api/events/{id}`, `/api/events/{eventId}/categories`, `/api/enrolments`, `/api/enrolments/{id}`, `/api/events/{eventId}/results`, `/api/results`, and `/api/results/{id}`. Note the spelling `enrolments` (one l after enrol) used by the actual API. The MVC app must forward the `.RaceDay.Session` cookie on authenticated requests.

## Run locally
`dotnet restore`
`dotnet run`

## Docker
From this folder:
`docker build -t raceday-mvc .`
`docker run --rm -p 8080:8080 -e ApiSettings__BaseUrl=http://host.docker.internal:7001/ raceday-mvc`

Change the API URL to an address reachable from the container.

## Required integration still to complete
The supplied brief requires Azure Blob Storage image uploads to be handled by the Part 2 API. The earlier Part 2 starter has no Azure Blob upload endpoints. Add secure API endpoints for event banner and participant profile image uploads, then add MVC upload forms/actions calling those endpoints. Do not connect MVC directly to Azure. The organiser dashboard, enrolment list with participant names/categories, result entry/publishing, participant results with event/category/rank context, filtering, and profile picture display also depend on corresponding API response fields/endpoints. Verify those API contracts against your Part 1 ERD and Part 2 implementation before submission.


## Integration notes based on the uploaded Part 2 API
- Registration payload fields must match the `RegisterRequest` record in Part 2: `FullName`, `Email`, `Password`, `Phone`, `Role`, `DateOfBirth`, `Gender`, `Club`, `EmergencyContact`, `EmergencyPhone`, `OrgName`, `ContactPerson`, and `Address`.
- Event categories are created with `POST /api/events/{eventId}/categories`; event ID is part of the route, not the JSON body.
- Enrolment uses `POST /api/enrolments` with `EventID` and `CategoryID`; listing uses `GET /api/enrolments`; cancellation uses `DELETE /api/enrolments/{id}`.
- Results are created with `POST /api/results` using `EnrolmentID`, `Position`, `FinishTime`, `TimeInSeconds`, and `Remarks`. The current API has no result update/delete endpoint and no participant-specific results endpoint. Do not present all event results as a participant's personal results. Add a secured API endpoint for a participant's own results and organiser result-management endpoints before marking those requirements complete.
- The uploaded Part 2 API currently has no Azure Blob Storage endpoints. Implement upload endpoints in Part 2 first; the MVC app must call those endpoints and must not connect to Azure directly.
- The uploaded Part 2 API currently uses minimal API endpoints and EF Core models in `Program.cs`; this MVC client does not use EF Core or connect to SQL Server.

This is an integration starter aligned to the actual routes found in the uploaded Part 2 `Program.cs`. It still requires local compilation and testing against the running API. Check all response JSON property names against Swagger before final submission.
