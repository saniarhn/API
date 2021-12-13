////console.log('array js running');


////const animals = [
////    { name: 'fluffy', species: 'cat', class: { name: 'mamalia' } },
////    { name: 'Nemo', species: 'fish', class: { name: 'vertebrata' } },
////    { name: 'hely', species: 'cat', class: { name: 'mamalia' } },
////    { name: 'Dory', species: 'fish', class: { name: 'vertebrata' } },
////    { name: 'ursa', species: 'cat', class: { name: 'mamalia' } }
////]


////const OnlyCat = [];
////for (let i = 0; i < animals.length; i++) {
////    if (animals[i].species == 'cat') {
////        OnlyCat.push( animals[i]);
////    }
////}
////console.log(OnlyCat);

////for (let i = 0; i < animals.length; i++) {
////    if (animals[i].species == 'fish') {
////        animals[i].class.name = 'Non - Mamalia';
////    }
////}

////console.log(animals);

/*var text = "";
$.ajax({
    url: "https://swapi.dev/api/people"
}).done((result) => {
    console.log(result.results);
    $.each(result.results, function (key, val) {
        text += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td>${val.gender}</td>
                    <td>${val.height}</td>
                    <td>${val.mass}</td>
                    <td><a class="btn btn-light text-info btn-sm" href="${val.films[0]}" role="button">Detail Film</a></td>
            
                   <td> <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-url="${val.url}" onclick="getData('${val.url}')">
                        Launch demo modal
                    </button></td>

                </tr>`;
    });
    $("#listSW").html(text);
}).fail((error) => {
    console.log(error);
});*/


var text = '';
/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    console.log(result.results);
    $.each(result.results, function (key, val) {
        text += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                   <td> <button type="button" class="btn btn-info" data-toggle="modal" data-target="#DetailPokemon" data-url="${val.url}" onclick="getData('${val.url}')">
                        Detail
                    </button></td>

                </tr>`;
    });
    $("#listPokemon").html(text);
}).fail((error) => {
    console.log(error);
});*/

function getData(url) {
    $.ajax({
        url: url
    }).done((result) => {
        console.log(result)
        var text = ''
        var type =''
        text = `<div class="text-center">
                <img class="rounded-circle bg-info" style="width:120px;height:120px;" src="${result.sprites.other["official-artwork"].front_default}">
                 <p class="bdg1">
                </p>
                  <table class=" table bg-light table-hover text-info text-center">
                    <tr >
                        <td>Name</td>
                        <td>:</td>
                        <td>${result.name}</td>
                    </tr>
                    <tr>
                        <td>Ability</td>
                        <td>:</td>
                        <td>${result.abilities[0].ability.name}</td>
                    </tr>
                    <tr>
                        <td>Height</td>
                        <td>:</td>
                        <td>${result.height}</td>
                    </tr>
                    <tr>
                        <td>Weight</td>
                        <td>:</td>
                        <td>${result.weight}</td>
                    </tr>

                    </table>
                </div>`
        $('.modal-body').html(text);
        $.each(result.types, function (key, val) {
            console.log(val.type.name)
            if (val.type.name == 'grass') {
                type += `<span class="badge badge-pill badge-success mr-2">${val.type.name}</span>`
            }
            else if (val.type.name == 'poison') {
                type += `<span class="badge badge-pill badge-dark mr-2">${val.type.name}</span>`
            }
            else if (val.type.name == 'fire') {
                type += `<span class="badge badge-pill badge-danger mr-2">${val.type.name}</span>`
            }
            else if (val.type.name == 'water') {
                type += `<span class="badge badge-pill badge-info mr-2">${val.type.name}</span>`
            }
            else if (val.type.name == 'bug') {
                type += `<span class="badge badge-pill badge-primary mr-2">${val.type.name}</span>`
            }
            else if (val.type.name == 'flying') {
                type += `<span class="badge badge-pill badge-warning mr-2">${val.type.name}</span>`
            }
            else if (val.type.name == 'normal') {
                type += `<span class="badge badge-pill badge-light mr-2">${val.type.name}</span>`
            }
        });
        $('.bdg1').html(type);
    }).fail((error) => {
        console.log(error);
    });
}
 $(document).ready(function () {
   var t = $("#table").DataTable({
        "ajax": {
            "url": "https://pokeapi.co/api/v2/pokemon/",
            "dataSrc": "results"
        },
        "columns": [
            {
                "data":null
            },
            {
                "data": "name"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-info" data-toggle="modal" 
                    onclick="getData('${row["url"]}')" data-target="#DetailPokemon">
                              Detail
                            </button>`;
    
                }
            }
        ]
   });
     t.on('order.dt search.dt', function () {
         t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
             cell.innerHTML = i + 1;
         });
     }).draw();

});

let g1 = document.getElementById("grid");

let col = document.getElementsByClassName("col");

g1.addEventListener('click', function () {
    g1.style.backgroundColor = 'darkgoldenrod';
    col[0].style.backgroundColor = 'yellow';
    col[4].style.fontStyle = "italic";
    col[3].style.fontFamily = "Impact,Charcoal,sans-serif";
});


$("#btn1").on('click', function () { alert("Hai Teman-teman!"); });



let btn2 = document.getElementById("btn2");
btn2.addEventListener('click', myFunction);


function myFunction() {

    document.getElementById("paragraf").innerHTML = "ini sebuah paragraf";
    var div1 = document.createElement('div');
    div1.classList.add('col', 'grid1');
    var div2 = document.createElement('div');
    div2.classList.add('col', 'grid2');
    var textnode1 = document.createTextNode("ini kolom 1");
    var textnode2 = document.createTextNode("ini kolom 2");
    div1.appendChild(textnode1);
    div2.appendChild(textnode2);
    document.getElementById("tambahtulisan").appendChild(div1);
    document.getElementById("tambahtulisan").appendChild(div2);

}


$("#btn3").on('click', function () {
    var tulisan = document.getElementById("tambahtulisan");
    while (tulisan.hasChildNodes()) {
        tulisan.removeChild(tulisan.firstChild);
    }
    document.getElementById("paragraf").innerHTML = " ";
});

$("span").mouseover(function () {
    $("span").css("background-color", "yellow");
});

$("span").mouseout(function () {
    $("span").css("background-color", "lightgrey");
});

$("#hide").click(function () {
    $("p").hide();
});

$("#show").click(function () {
    $("p").show();
});

