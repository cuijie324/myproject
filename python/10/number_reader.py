import json

filename = 'numbers.json'
with open(filename) as f_object:
    numbers = json.load(f_object)

print(numbers)
