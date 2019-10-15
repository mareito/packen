El desarrollo del proyecto se hizo utilizando visual studio 2017, utilizando el framework .NET CORE 2.2 y ENTITY FRAMEWORK CORE

La Aplicacion trabaja sobre una BD LOCAL en MYSQL para su correcto funcionamiento se deberian hacer los siguientes pasos: 

- Crear una BD llamada packen.
- Ejecutar el SQL adjunto.
- Abrir y ejecutar el proyecto con visual estudio 
- Al correr el proyecto se despliega la pagina con la documentacion de swagger. 

El proyecto tiene los siguientes servicios: 


- https://localhost:44351/api/Autentication/Login:  (POST)
  Este servicio devuelve el token firmado para la seguridad de los servicios protegidos. para   ingresar 
  Body : 
  {
    "email": "test",
    "password": "test"
  }

- https://localhost:44351/api/BowlingScore: (POST) 
  Serivicio para calcular el puntaje final para el juego de bolos.
  Body:
  {
    "rollsSequence": "XXXXXXXXXXXX"
  }

- https://localhost:44351/api/BowlingScore/secure: (POST)
  Servicio para calcular el puntaje final para los usuarios logeados. Se debe enviar en el request el header con el nombre "Token" y el token retornado por el servicio de login. 
  Body:
  {
    "rollsSequence": "XXXXXXXXXXXX"
  } 

- https://localhost:44351/api/BowlingScore/secure/TopGames?NumRows=3 (GET) 
  Servicio que retorna los juegos con los puntajes mas altos de un jugador. Se debe enviar en el request el header con el nombre "Token" y el token retornado por el servicio de login. Los registros retornados corresponden al jugador identificado en el token. 
 
  