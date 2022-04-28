var books = require('./books')

exports.assignRoutes = function (app) {
    app.get('/books', books.getbooks);
    app.get('/books/:bookId', books.getbookbyId);
    app.post('/books', books.addbook);
    app.put('/books/:bookId', books.updatebook);
    app.delete('/books/:bookId', books.deletebook);
}