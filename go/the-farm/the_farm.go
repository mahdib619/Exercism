package thefarm

import (
	"errors"
	"fmt"
)

var (
	ErrNegativeFodder = errors.New("negative fodder")
	ErrDivisionByZero = errors.New("division by zero")
)

type SillyNephewError struct {
	cows int
}

func (err *SillyNephewError) Error() string {
	return fmt.Sprintf("silly nephew, there cannot be %d cows", err.cows)
}

func NewSillyNephewError(cows int) *SillyNephewError {
	return &SillyNephewError{
		cows: cows,
	}
}

// DivideFood computes the fodder amount per cow for the given cows.
func DivideFood(weightFodder WeightFodder, cows int) (float64, error) {
	fodder, err := weightFodder.FodderAmount()

	if fodder < 0 && (err == nil || err == ErrScaleMalfunction) {
		return 0, ErrNegativeFodder
	}

	if err == ErrScaleMalfunction {
		fodder *= 2
	} else if err != nil {
		return 0, err
	}

	if cows == 0 {
		return 0, ErrDivisionByZero
	}

	if cows < 0 {
		return 0, NewSillyNephewError(cows)
	}

	return fodder / float64(cows), nil
}
