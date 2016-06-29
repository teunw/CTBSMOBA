import json

from django.core import serializers
from django.http import JsonResponse, HttpResponse

from DashData.models import Character
from django.contrib.auth.models import User


def characters(request):
    dictionaries = [obj.as_dict() for obj in Character.objects.get_queryset()]
    return HttpResponse(json.dumps({"characters": dictionaries}), content_type='application/json')


def login(request):
    username = request.GET["username"]
    password = request.GET["password"]
    try:
        user = User.objects.get(username=username)
    except User.DoesNotExist:
        return JsonResponse({"error": "User doesn't exist"})

    passwordCorrect = user.check_password(password)

    if not passwordCorrect:
        return JsonResponse({"error": "Incorrect password"})

    userCharacters = Character.objects.get(owner=user)
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

    Character.objects.create(owner=user, name=user.username)

    return JsonResponse(
        {
            "success": True,
            "user": user.username
        }
    )


def old_data(request):
    return characters(request)