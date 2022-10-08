package chessboard

type (
	File       []bool
	Chessboard map[string]File
)

// CountInFile returns how many squares are occupied in the chessboard,
// within the given file.Chessboard
func CountInFile(cb Chessboard, file string) int {
	occupiedCount := 0
	for _, square := range cb[file] {
		if square {
			occupiedCount++
		}
	}
	return occupiedCount
}

// CountInRank returns how many squares are occupied in the chessboard,
// within the given rank.
func CountInRank(cb Chessboard, rank int) int {
	if rank < 1 || rank > 8 {
		return 0
	}

	occupiedCount := 0
	for _, file := range cb {
		if file[rank-1] {
			occupiedCount++
		}
	}
	return occupiedCount
}

// CountAll should count how many squares are present in the chessboard.
func CountAll(cb Chessboard) int {
	return 64
}

// CountOccupied returns how many squares are occupied in the chessboard.
func CountOccupied(cb Chessboard) int {
	total := 0
	for file := range cb{
		total +=  CountInFile(cb, file)
	}
	return total
}
