from django.db import models

# Create your models here.
from django.db.models import Model

class Character(models.Model):
    TIEDTOGETHER = "Tiedtogether"
    KICK = "Kick"
    GROW = "Grow"

    SKILL_CHOICES = (
        (TIEDTOGETHER, TIEDTOGETHER), (KICK, KICK), (GROW, GROW)
    )

    name = models.CharField(primary_key=True, max_length=32)
    speed = models.FloatField()
    stamina = models.FloatField()
    skill1 = models.CharField(
        max_length=32,
        choices=SKILL_CHOICES,
        default=KICK
    )
    skill2 = models.CharField(
        max_length=32,
        choices=SKILL_CHOICES,
        default=TIEDTOGETHER
    )

    def __str__(self):
        return self.name + " [" + self.speed.__str__() + " Speed:" + self.stamina.__str__() + " Stamina]"