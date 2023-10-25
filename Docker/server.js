
const mysql = require('mysql');

const connection = mysql.createConnection({
  host : 'mysql_db',
  user : 'blackDot',
  password: 'dot',
  database : 'TheBlackThing'
});

connection.connect((err) => {
  if (err) {
    console.error('Error connecting to MySQL:', err);
    return;
  }
  console.log('Connected to MySQL');
});