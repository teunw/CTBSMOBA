from django.http import JsonResponse

from DashData.models import Character


def characters(request):
    return JsonResponse(
        dict(
            list(
                Character.objects.values("name", "speed", "stamina", "skill1", "skill2")
            )
        )
    )
