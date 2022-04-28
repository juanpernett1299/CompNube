var books = [
    {"id":1, "title": "La Hojarasca", "description": "Good one", "author": "Gabo"},
    {"id":2, "title": "El coronoel no tiene quien le escriba", "description": "Interesting", "author": "Gabo"},
    {"id":3, "title": "El coronoel no tiene quien le escriba", "description": "Interesting", "author": "Gabo"}
];

var id=books.length
//Req = peticion :: Res = respuesta

exports.getbooks = function(req, res) {
    res.send(books);
}

exports.getbookbyId = function(req, res){
    var bookId = parseInt(req.params.bookId);

    for (var bookIndex in books) {
        var book = books[bookIndex];
        if(book.id === bookId){
            res.send(book)
            break
        }
    }
}

exports.addbook = function(req, res) {
    var title = req.body.title;
    //var description = req.body.description;
    //var author = req.body.author;

    books.push({'id':  id+1 , 'title': title, 'description': '', 'author': ''});
    res.send(books);
}

exports.updatebook = function(req, res){
    var bookId = parseInt(req.params.bookId);

    for (var bookIndex in books) {
        var book = books[bookIndex];

        if (book.id === bookId) {
            book.title = req.body.title
            book.description=req.body.description
            book.author=req.body.author
            break;
        }
    }

    res.send(books)
}


exports.deletebook = function(req, res) {
    var bookId = parseInt(req.params.bookId);

    for (var bookIndex in books) {
        var book = books[bookIndex];

        if (book.id === bookId) {
            books.splice(bookIndex, 1);
            break;
        }
    }

    res.send(books);
}