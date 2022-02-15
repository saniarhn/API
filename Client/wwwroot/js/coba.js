// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


////console.log('array js running');


const animals = [
   { name: 'fluffy', species: 'cat', class: { name: 'mamalia' } },
   { name: 'Nemo', species: 'fish', class: { name: 'vertebrata' } },
    { name: 'hely', species: 'cat', class: { name: 'mamalia' } },
    { name: 'Dory', species: 'fish', class: { name: 'vertebrata' } },
   { name: 'ursa', species: 'cat', class: { name: 'mamalia' } }
]


const OnlyCat = [];
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'cat') {
        OnlyCat.push( animals[i]);
   }
}
console.log(OnlyCat);

for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == 'fish') {
        animals[i].class.name = 'Non - Mamalia';
    }
}

console.log(animals);