# Game Library Manager V2 API Spec
----
This API exists to decouple the front end of the Game Library Manager (henceforce referred to as GLM) from the front-end. The first version of this software was written in in VBNET with SQL integration in late 2020. The application was built with the frontend as a Windows Forms app that was deeply coupled with its SQL-based backend. This deep coupling made it very constly to implement new features, such as adding new genres or consoles I didn't account for in the original implementation. In this remake of the application, I hope to achieve decoupling of the stack via this API.

## API Resources
----
These are the resources that the frontend will be able to access. They are interactable as to make the Game schema extensible. 

* **Genre**: A genre represents the classification of a game into its gameplay genre. It will have a one to many relationship to the game resource.

* **Console**: The release platform. It will have a one-to-many relationship with games as a game can only exist on one console. It is important to note that while games do have multiple-platform releases, games in this context refer to an entry in the user's collection. 

* **Game**: The game resource is a composite of the other two resources (Genre and Software) as well as attributes such as title, format, average time to complete, release year, etc.

## Data Tables
----
Proposed data tables within the data store

### Game
```
Id - Guid (PK)
Title - String
Status - Enumified String
ConsoleID - Guid (FK Console)
Format - Enumified String
Genre - Guid (FK Genre)
AverageLength - Int 
YearOfRelease - Int
LoggedAt - DateTime
FinishedAt - DateTime
Notes - String
Image - Image (from GiantBomb API)
```

### Console
```
ConsoleID - Guid
Console - String
```

### Genre
```
GenreID - Guid
Genre - String
```

##Data Models
----
These definitions are reflective of the models to be used when constructing the data layer of the API. No fields are optional. 

### Game
```
GameID - Guid
Title - String
Status - Status
Console - String
Format - Format
Genre - String
AverageLength - Int
Year of Release - Int
LoggedAt - DateTime
CompletedAt - DateTime
Notes - String
```

### Enum Status
```
Unfinished
Playing
Finished
```

### Enum Format
```
Digital
Physical
Special Edition
```

### Console
```
ConsoleID - Guid
Console - String
```

### Genre
```
GenreID - Guid
Genre - String
```

##Endpoints
----
### Library
---
#### Fetch Whole Library
---
**Request**:
```
GET /library
```

Parameters: 
```
Parameter: Filter
Name: Status
Possible Values: Unfinished, Playing, Incomplete
Optional: Yes
Description: Filters by current status in library

Parameter: Filter
Name: Console
Possible Values: Any console the user has registered
Optional: Yes
Description: Filters by console

Parameter: Filter
Name: Format
Possible Values: Physical, Digital, Special Edition
Optional: Yes
Description: Filters by format of entry

Parameter: Filter
Name: Genre
Possible Values: Any genre the user has registered
Optional: Yes
Description: Filters by current status in library

Parameter: SortBy
Name: Title
Possible Values: Title; asc, desc
Optional: Yes
Description: Sorts alphabetically by title

Parameter: SortBy
Name: Console
Possible Values: Console; asc, desc
Optional: Yes
Description: Sorts alphabetically by console

Parameter: SortBy
Name: Genre
Possible Values: Genre; asc, desc
Optional: Yes
Description: Sorts alphabetically by genre

Parameter: SortBy
Name: AverageLength
Possible Values: AverageLength; asc, desc
Optional: Yes
Description: Sorts numerically by average length

Parameter: SortBy
Name: YearOfRelease
Possible Values: YearOfRelease; asc, desc
Optional: Yes
Description: Sorts chronologically by year of release

Parameter: SortBy
Name: Dates
Possible Values: LoggedAt, CompletedAt; asc, desc
Optional: Yes
Description: Sorts chronologically by logged date or completed date
```
Body: None

**Response** - 200 OK
```
{
    "meta" : {
        "entryCount": x
    },
    "data" : [
        {
            "Id": "d56ba830-61ec-4695-a935-2c81d1059d62"
            "title": "Elden Ring"
            "status": "Playing"
            "console": "Playstation 5"
            "format": "Physical"
            "genre": "Soulslike"
            "averageLength": 49 
            "yearOfRelease" - 2022
            "loggedAt" - "2022-03-22T18:30:00Z"
            FinishedAt - null
            Notes - null
        },
        {
            "Id": "86f20688-5117-4641-8784-676f890c7531"
            "title": "Pokemon Emerald"
            "status": "Finished"
            "console": "GameBoy Advance"
            "format": "Physical"
            "genre": "JRPG"
            "averageLength": 31 
            "yearOfRelease" - 2004
            "loggedAt" - "2005-07-06T18:30:00Z"
            FinishedAt - "2005-09-06T18:30:00Z"
            Notes - "need to go back and catch them all at some point"
        },
        .
        .
        .
    ]
}
```

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```

#### Fetch Single Entry
---
**Request**:
```
GET /library/{id}
```

Body: None

**Response** - 200 OK
```
{
    "meta" : {
        "entryCount": x
    },
    "data" : [
        {
            "Id": "5ed697a5-2a07-48e6-9e51-6aee0d3265f3"
            "title": "Dragon Quest II"
            "status": "Unfinished"
            "console": "Nintendo Entertainment System"
            "format": "Physical"
            "genre": "JRPG"
            "averageLength": 18 
            "yearOfRelease" - 1987
            "loggedAt" - "2021-02-22T18:30:00Z"
            FinishedAt - null
            Notes - null
        }
    ]
}
```

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```


#### Log New Entry
---
**Request**:
```
POST /library
```
Body:
```
{
    "meta" {},
    "data": {
            "title": "Dragon Quest II"
            "status": "Unfinished"
            "console": "Nintendo Entertainment System"
            "format": "Physical"
            "genre": "JRPG"
            "averageLength": 18 
            "yearOfRelease" - 1987
            Notes - null
    }
}
```

**Response** - 201 CREATED
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```


#### Update Entry
---
**Request**:
```
PUT /library/{id}
```
Body:
```
{
    "meta" {},
    "data": {
            "title": "Xenoblade Chronicles: Definitive Edition"
            "status": "Unfinished"
            "console": "Nintendo Switch"
            "format": "Special Edition"
            "genre": "JRPG"
            "averageLength": 70 
            "yearOfRelease" - 2020
            Notes - "Finished original version"
    }
}
```

**Response** - 200 OK
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```

#### Delete Entry
---
Request:
```
DELETE /library/{id}
```
Body: None

**Response** - 200 OK
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```

### Genre
----
#### Get All Genres
---
**Request**:
```
GET /genre
```

Parameters: 
```
Parameter: Sort
Name: Name
Possible Values: Name
Optional: No
Description: Sorts alphabetically by genre name
```
Body: None

**Response** - 200 OK
```
{
    "meta" : {},
    "data" : [       
        {
            "Id": "86f20688-5117-4641-8784-676f890c7531"
            "genre" : "Bullet Hell"
        },
        {
            "id": "d56ba830-61ec-4695-a935-2c81d1059d62"
            "genre" : "Character-Action"
        },
        .
        .
        .
    ]
}
```

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```


#### Add New Genre
---
**Request**:
```
POST /genre
```
Body: 
```
{
    "meta" : {},
    "data" : {
        "genre" : "Western RPG"
        }
}
```

**Response** - 201 CREATED
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"}
}
```


#### Update Genre
---
**Request**:
```
PUT /genre/{id}
```
Body: 
```
{
    "meta" : {},
    "data" : {
        "genre" : "Western RPG"
        }
}
```

**Response** - 200 OK
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"}
}
```


#### Delete Genre
---
**Request**:
```
DELETE /genre/{id}
```
Body: None

**Response** - 200 OK
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"}
}
```

### Console
----
#### Get All Consoles
---
**Request**:
```
GET /console
```

Parameters: 
```
Parameter: Sort
Name: Name
Possible Values: Name
Optional: No
Description: Sorts alphabetically by console name
```
Body: None

**Response** - 200 OK
```
{
    "meta" : {},
    "data" : [       
        {
            "Id": "86f20688-5117-4641-8784-676f890c7531"
            "console" : "Sega Dreamcast"
        },
        {
            "id": "d56ba830-61ec-4695-a935-2c81d1059d62"
            "console" : "Nintendo Entertainment System"
        },
        .
        .
        .
    ]
}
```

Error Codes:
4XX Client Error
```
{
    message: "string"
}
```


#### Add New Console
---
**Request**:
```
POST /console
```
Body: 
```
{
    "meta" : {},
    "data" : {
        "console" : "Atari 2600"
        }
}
```

**Response** - 201 CREATED
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"}
}
```


#### Update Console
---
**Request**:
```
PUT /console/{id}
```
Body: 
```
{
    "meta" : {},
    "data" : {
        "console" : "Xbox 360"
        }
}
```

**Response** - 200 OK
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"}
}
```


#### Delete Console
---
**Request**:
```
DELETE /console/{id}
```
Body: None

**Response** - 200 OK
Body: None

Error Codes:
4XX Client Error
```
{
    message: "string"}
}
```
