const axios = require("axios");

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
const getUrl = "https://localhost:44333/api/Agenda";

//GET All
/*const getAll = () =>
    axios.get(getUrl)
        .then((res) => console.log(res.data))
        .catch(console.error); */

//GET BY ID
/*
var identify = 1;
axios.get(`${getUrl}/${identify}`)
.then((res) => console.log(res.data))
.catch(console.error); 
*/

//post
/*
const body = {    
    "primeiroNome": "Gabriel",
    "ultimoNome": "Monarca",
    "telefone": 3333,
    "idade": 15,
    "cep": "81220400"
    
}
axios.post(getUrl, body)
.then((res) => console.log(res.data))
.catch(console.error); 
*/

var deletee = 1;
axios.delete(`${getUrl}/${deletee}`)
.then((res) => console.log(res.data))
.catch(console.error); 












