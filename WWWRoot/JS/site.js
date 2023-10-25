const uri = 'https://localhost:7124/api/attack';
let todos = [];
let input1 = document.querySelector('add-name');
let input2 = document.querySelector('add-description');
let input3 = document.querySelector('add-type');
let input4 = document.querySelector('add-hitvalue');
let input5 = document.querySelector('add-apcost');
const button = document.querySelector('#post')

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addDescriptionTextbox = document.getElementById('add-description');
    const addTypeTextbox = document.getElementById('add-type');
    const addHitValueTextbox = document.getElementById('add-hitvalue');
    const addAPCostTextbox = document.getElementById('add-apcost');
    const item = {
        name: addNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim(),
        type: addTypeTextbox.value.trim(),
        hitvalue: addHitValueTextbox.value.trim(),
        apcost: addAPCostTextbox.value.trim(),
      };
    
      fetch(uri, {
        method: 'POST',
        mode: 'cors',
        headers: {
          'Content-Type' : 'application/json',
        },
        body: JSON.stringify(item)
      })
        .then(response => response.json())
        .then(data => {

          console.log('Received data from the server.', data);

          console.log('Id: ', data.Id);
          console.log('Name: ', data.Name);

          getItems();
          addNameTextbox.value = '';
          addDescriptionTextbox.value = '';
          addTypeTextbox.value = '';
          addHitValueTextbox.value = '';
          addAPCostTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
    }

button.addEventListener('click',function(e){
    e.preventDefault();
    addItem()
})
