"""Double color resistor calculator"""

COLORS = {"black": 0,
          "brown": 1,
          "red": 2,
          "orange": 3,
          "yellow": 4,
          "green": 5,
          "blue": 6,
          "violet": 7,
          "grey": 8,
          "white": 9}


def value(colors):
    val_str = f'{COLORS[colors[0]]}{COLORS[colors[1]]}'
    return int(val_str)
