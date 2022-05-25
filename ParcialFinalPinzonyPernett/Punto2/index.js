const express = require('express')
const app = express()
const port = 3000
app.use(express.json())
let books = [
	{
		id: 1,
		title: 'La hojarasca',
		description: 'Good one',
		author: 'Gabo'
	},
	{
		id: 2,
		title: 'El coronel no tiene quien le escriba',
		description: 'Interesting',
		author: 'Gabo'
	},
	{
		id: 3,
		title: 'El coronel no tiene quien le escriba',
		description: 'Interesting',
		author: 'Gabo'
	}
]
app.get('/books', function (req, res) {
	res.status(200).json({
		books: books
	})
})
app.get('/books/:book_id', function (req, res) {
	const { book_id } = req.params
	const book = books.find(book => book.id === parseInt(book_id))
	if (!book) {
		res.status(404).send('Not Found')
		return
	}
	res.status(200).json({ book })
})
app.post('/books', function (req, res) {
	const { body: book } = req
	if (!book.title) {
		res.status(400).json({ message: 'Title is required' })
		return
	}
	const row = {
		id: books.length + 1,
		title: book.title,
		description: book.description || '',
		author: book.author || ''
	}
	books.push(row)
	res.status(201).json({
		book: row
	})
})
app.put('/books/:book_id', function (req, res) {
	const { book_id } = req.params
	const { body } = req
	const index = books.findIndex(book => book.id === parseInt(book_id))
	if (index === -1) {
		res.status(404).send('Not Found')
		return
	}
	const updatedBook = { ...books[index], ...body }
	books[index] = updatedBook
	res.status(200).json({
		book: updatedBook
	})
})
app.delete('/books/:book_id', function (req, res) {
	const { book_id } = req.params
	const index = books.findIndex(book => book.id === parseInt(book_id))
	if (index === -1) {
		res.status(404).send('Not Found')
		return
	}
	books.splice(index, 1)
	res.status(200).json({
		result: true
	})
})
app.listen(port, () => {
	console.log(`App listening at http://localhost:${port}`);
})