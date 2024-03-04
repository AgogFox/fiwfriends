# FewFriends

## Planning

- Web design concept
- Features
- API
- Role
- Knowledge needed
- Timeline

## Concept

https://www.meetup.com

## Features

- Landing page
  - Login/Signup
  - Change language
  - Search box
  - Introduction to website
  - Explore before login
    - location
- Login/Signup
  - Simple username-password login
- Landing page for Signed in user
  - Personalized greeting message
  - Upcoming events
  - Filter
    - Date {Tomorrow, Next week, Weekend, ...}
    - Type {Online, Onsite}
  - Save event
  - Your groups (optional)
  - Other event from your group (optional)
- Search
  - Topic(keywords)
  - Name
  - Location
  - Filter
    - Date {Tomorrow, Next week, Weekend, ...}
    - Type {Online, Onsite}
- Events
  - Hosted by
  - Date/Time
  - Registration date
  - Minimum Attendees
  - Expiry date
  - Location
    - Address
    - City, Country
    - Map (option)
  - Pictures
  - Description
    - Rich text editor (optional)
  - Attendees
    - Attendees count
    - list
  - Keywords
  - Summary
    - Title
    - Spot left
    - Attend button
    - Price (optional)
  - Report (optional)
- User profile
  - Profile picture
  - Base location
  - Interests(keywords)
  - About me
  - Photos that user post
    - Description
    - Event
  - RSVPs (optional)
- Setting
  - Edit profile
    - Change profile picture
    - Name
    - Base location
    - Bio
    - ~~Show/Hide infomation~~
  - Security
    - Password
    - Delete account
  - Interests

## API

- Login/Signup
  - username: `string`
  - password: `string`
- After login
  - username: `string`
  - session_token ???: `string`
- Save event
  - event_id: `int`
  - session_token ???: `string`
- Filter !!!PENDING!!!
  - date
  - type
- Search !!!PENDING!!!
  - topic
  - name
  - location
- Event (Single event)
  - host_by: `string`
  - date_time: `string ISO 8601`
  - location: {address: `string`, city_country: `string city, ISO 3166-1 alpha-2`}
  - picture: `string href`
  - description: `string`
  - attendees:
    \[{username: `string`, profile_pic: `string href`}, {username: `string`, profile_pic: `string href`}\]
  - keywords: `array of string`
  - spots: `int`
  - spots_left: `int`
- User profile
  - picture: `string href`
  - location: {address: `string`, city_country: `string city, ISO 3166-1 alpha-2`}
  - keywords: `array of string`
  - bio: `string`
  - posts {picture: `string href`, desc: `stinrg`, event_id: `int`}
- Setting
  - Edit profile
    - picture: `string href`
    - location: {address: `string`, city_country: `string city, ISO 3166-1 alpha-2`}
    - bio: `string`
  - Security
    - Change password
      - {current_pass: `string`, new_pass: `string`}
    - Delete account
      - _normal api call_
  - Interests
    - keywords: `array of string`

## Role

Tae: Back -> Front
Bo: Front
Mon: Front -> Back
Pearwa: Back -> Font
In: Back
