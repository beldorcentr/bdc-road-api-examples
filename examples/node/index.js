const axios = require('axios');
const querystring = require('querystring');

const body = querystring.stringify({
  grant_type: 'password',
  client_id: 'bdc_road_api_resource_owner',
  username: 'USERNAME',
  password: 'PASSWORD'
});

const config = { 
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded'
  }
};

axios.post('https://i.centr.by/oauth2/connect/token', body, config)
.then(token => {
  axios.get('https://i.centr.by/bdc-road-api/v1.0/roads/30000001', {
    headers: {
      'Authorization': 'Bearer ' + token.data.access_token
    }
  })
  .then(response => {
    console.log(response.data);
  }, err => {
    console.log(err);
  });
}, err => {
  console.log(err);
});
