var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

server.listen(3000, function(){console.log('--server is running--')});

// global
var enemies = [];
var playerSpawnPoints = [];
var clients = [];

io.on('connection', function(socket){
  var currentPlayer = {};
  currentPlayer.name = 'unknown';

  socket.on('player connect', function(){
    console.log(currentPlayer.name + ' recv player connect');
    for(var i=0; i<clients.length; i++){
      var playerConnected = {
        name: clients[i].name,
        position: clients[i].position,
        rotation: clients[i].rotation,
        health: clients[i].health
      }
      socket.emit('other player connected', playerConnected);
      console.log(currentPlayer.name + ' emit: other player connected: ' + JSON.stringify(playerConnected));
    }
  });

  socket.on('play', function(data){
    console.log(currentPlayer.name + ' recv: play: ' + JSON.stringify(data));

    if(clients.length === 0){
      numberOfEnemies = data.enemySpawnPoints.length;
      enemies = [];
      data.enemySpawnPoints.forEach(enemySpawnPoint => {
        var enemy = {
          name: guid(),
          position: enemySpawnPoint.position,
          rotation: enemySpawnPoint.rotation,
          health: 100
        }
        enemies.push(enemy);
      });
      playerSpawnPoints = [];
      data.playerSpawnPoints.forEach(function(_playerSpawnPoint){
        var playerSpawnPoint = {
          position: _playerSpawnPoint.position,
          rotation: _playerSpawnPoint.rotation
        }
        playerSpawnPoints.push(playerSpawnPoint);
      });

      var enemiesResponse = {
        enemies: enemies
      }
      console.log(currentPlayer.name + ' emit: enemies: ' + JSON.stringify(enemiesResponse));
      socket.emit('enemies', enemiesResponse);
      var randomSpawnPoint = playerSpawnPoints[Math.random() * playerSpawnPoints.length];
      currentPlayer = {
        name: data.name,
        position: randomSpawnPoint.position,
        rotation: randomSpawnPoint.rotation,
        health: 100
      }

      clients.push(currentPlayer);
      console.log(currentPlayer.name + ' emir: play : ' + JSON.stringify(currentPlayer));
      socket.emit('play', currentPlayer);
      socket.broadcast.emit('other player connected', currentPlayer);
    }
  });

  socket.on('player move', function(data){
    console.log('recv: move: ' + JSON.stringify(data));
    currentPlayer.position = data.position;
    socket.broadcast.emit('player move', currentPlayer);  // 다른 모든 클라이언트들에게 전송
  });

  socket.on('player turn', function(data){
    console.log('recv: turn: ' + JSON.stringify(data));
    currentPlayer.rotation = data.rotation;
    socket.broadcast.emit('player turn', currentPlayer);
  });

  socket.on('player shoot', function(data){
    console.log(currentPlayer.name + ' recv: shoot');
    var data = {
      name: currentPlayer.name
    }
    console.log(currentPlayer.name + ' brodecast: shoot: ' + JSON.stringify(data));
    socket.emit('player shoot', data);
    socket.broadcast.emit('player shoot', data);
  });

  socket.on('health', function(data){
    console.log(currentPlayer.name + ' recv: health: ' + JSON.stringify(data));

    if(data.from === currentPlayer.name){
      var indexDamaged = 0;
      if(!data.isEnemy){
        clients = client.map(function(client, index){
          indexDamaged = index;
          enemy.health -= data.healthChange;
        });
        return client;
      }else{
        enemies = enemies.map(function(enemy, index){
          if(enemy.name === data.name){
            indexDamaged = index;
            enemy.health -= data.healthChange;
          }
          return enemy;
        });
      }

      var response = {
        name: (!data.inEnemy)?clients[indexDamaged].name : enemies[indexDamaged].name,
        health: (!data.isEnemy)?clients[indexDamaged].health: enemies[indexDamaged].health
      }
      console.log(currentPlayer.name + ' recv: health: ', + JSON.stringify(response));
      socket.emit('health', response);
      socket.broadcast.emit('health', response);
    }
  });

  socket.on('disconnect', function(){
    console.log(currentPlayer.name + ' recv: disconnect ' + currentPlayer);
    socket.broadcast.emit('other player disconnected', currentPlayer);
    console.log(currentPlayer.name + 'bcst : other player disconnected ' + JSON.stringify(currentPlayer));
    for(var i=0; i<clients.length; i++){
      if(clients[i].name === currentPlayer.name){
        clients.splice(i, 1);
      }
    }
  });
});

function guid(){
  function s4(){
    return Math.floor((1+Math.random()) * 0x10000).toString(16).substring(1);
  }
  return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}

app.get('/', function(req, res){
  res.send('hey you got back get "/"');
});
// console.log('--server is running--' + guid());