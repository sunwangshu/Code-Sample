# Personal Space
![alt text](img/tb2.png?raw=true)

## Intro
This is a series of classes in Personal Space project made with Unity.
Project Link: http://www.sunwangshu.com/portfolio/personal-space/

## Model
The basic idea is that you want to maintain a good space with other people, otherwise you either feel shameful when you get too close, or feel lonely when you lose contact with people.

The picture below show the green player entering the white comfort zone of another person, before entering the red alter zone of them.

![alt text](img/Personal Space Model.jpeg?raw=true)

**Player.cs**: Player life cycle, player shaming or cooling and color transitions, zone interactions with other pedestrians.

**Zone.cs**: A universal zone for either *comfort zone* or *alert zone*, depending on their specific tag in Unity.

**ZoneRim.cs**: To show player the zone range, make the zone rim slightly bigger than the actual zone, and gradually show up zone a bit earlier before making direct contact.
