import json

filename = 'username.json'

# try:
#     with open(filename) as f_obj:
#         username = json.load(f_obj)
# except FileNotFoundError:
#     username = input("What is your name? ")
#     with open(filename, 'w') as f_obj:
#         json.dump(username, f_obj)
#         print("We'll remember you when you come back, " + username + "!")
# else:
#     print("Welcome back, " + username + "!")


def get_stored_username(filename):
    try:
        with open(filename) as f_obj:
            username = json.load(f_obj)
    except FileNotFoundError:
        username = None

    return username


def get_new_username(filename):
    username = input("What is your name?")
    with open(filename, 'w') as f_obj:
        json.dump(username, f_obj)

    return username


username = get_stored_username(filename)
if username:
    print("Welcome back, " + username + "!")
else:
    username = get_new_username(filename)
    print("We'll remember you, " + username + "!")
