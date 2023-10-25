const uri = 'https://localhost:7124/api/attack';

const button = document.querySelector('#post')

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addDescriptionTextbox = document.getElementById('add-description');
    const addTypeTextbox = document.getElementById('add-type');
    const addHitValueTextbox = document.getElementById('add-hitvalue');
    const addAPCostTextbox = document.getElementById('add-apcost');
    
    const type = parseInt(addTypeTextbox.value, 10);
    const hitvalue = parseInt(addHitValueTextbox.value, 10);
    const apcost = parseInt(addAPCostTextbox.value, 10);
    
    const item = {
        name: addNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim(),
        type: type,
        hitvalue: hitvalue,
        apcost: apcost,
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

          // getItems(); this needs to be built out
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
