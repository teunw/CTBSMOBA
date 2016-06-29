import json

from django.http import JsonResponse, HttpResponse
from django.utils.datastructures import MultiValueDictKeyError

from DashData.models import Character
from django.contrib.auth.models import User


def characters(request):
    try:
        dictionaries = [obj.as_dict() for obj in Character.objects.get_queryset()]
    except Exception:
        return JsonResponse({"error": "Could not retrieve characters, you can still play in offline mode "})
    return HttpResponse(json.dumps({"characters": dictionaries}), content_type='application/json')


def login(request):
    try:
        username = request.GET["username"]
        password = request.GET["password"]
    except MultiValueDictKeyError:
        return JsonResponse({"error": "Not all parameters were sent"})

    try:
        user = User.objects.get(username=username)
    except User.DoesNotExist:
        return JsonResponse({"error": "User doesn't exist"})

    passwordCorrect = user.check_password(password)

    if not passwordCorrect:
        return JsonResponse({"error": "Incorrect password"})

    try:
        userCharacters = Character.objects.get(owner=user)
    except Character.DoesNotExist:
        return JsonResponse({"error": "User character not found"})

    return JsonResponse(
        {
            "success": passwordCorrect,
            "username": user.username,
            "character": {
                "name": userCharacters.name,
                "speed": userCharacters.speed,
                "stamina": userCharacters.stamina,
                "skill1": userCharacters.skill1,
                "skill2": userCharacters.skill2,
            }
        }
    )


def register(request):
    checkUser = User.objects.filter(username=request.GET["username"]).exists()

    if checkUser:
        return JsonResponse({"error": "User already exists"})

    user = User.objects.create_user(
        request.GET["username"],
        None,
        request.GET["password"]
    )
    user.save()

    userCharacters = Character.objects.create(owner=user, name=user.username)

    return JsonResponse(
        {
            "success": True,
            "user": user.username,
            "character": {
                "name": userCharacters.name,
                "speed": userCharacters.speed,
                "stamina": userCharacters.stamina,
                "skill1": userCharacters.skill1,
                "skill2": userCharacters.skill2,
            }
        }
    )


def old_data(request):
    return characters(request)
